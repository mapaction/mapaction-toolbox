pipeline {
    agent any

    options {
        timeout(time: 10, unit: 'MINUTES')
        disableConcurrentBuilds()
        // timestamps()
    }
    
    triggers {
        // nightly build at ~0500hrs
        cron('H H(4-6) * * *')
        // pollSCM every ten minuites
        pollSCM('H/10 * * * *')
    }
    
    stages {
        stage ('PreBuild'){
            steps {
                node ('MA-JENKINS52') {
                    ws('%BUILD_TAG%') {
                        echo testing some env varibles
                        echo BUILD_TAG env.BUILD_TAG
                        echo WORKSPACE env.WORKSPACE
                        echo JOB_NAME env.JOB_NAME
                        echo BUILD_DISPLAY_NAME env.BUILD_DISPLAY_NAME
                        echo BUILD_NUMBER env.BUILD_NUMBER
                        echo about to clone git
                        checkout([$class: 'GitSCM', branches: [[name: '**']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[url: 'https://github.com/mapaction/mapaction-toolbox.git']]])

                        // Set Github status to "pending".
                        // Use curl atm, since it is copying from the pre-Jenkinsfile config.
                        // TODO: test whether on not we can use `setGitHubPullRequestStatus`
                        // TODO: refactor the auth token to an variable to something.
                        bat '"C:\\Program Files (x86)\\Git\\bin\\curl.exe" -XPOST -H "Authorization: token github_mapaction_jenkins" https://api.github.com/repos/mapaction/mapaction-toolbox/statuses/%GIT_COMMIT% -d "{ \\"state\\": \\"pending\\",  \\"target_url\\": \\"%BUILD_URL%\\", \\"description\\": \\"JENKINS: The build and tests are pending.\\" }"'
                    }
                }
            }
        }

        stage('Build') {
            steps {
                node ('MA-JENKINS52') {
                    ws('%BUILD_TAG%') {
                        echo 'Building..'

                        // Do the MSbuild
                        bat 'C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe /t:build /p:PlatformTarget=x86 /p:Configuration=Release /maxcpucount arcgis10_mapping_tools/MapAction-toolbox.sln'
                        
                        bat '%WORKSPACE%\\arcgis10_mapping_tools\\arcaddins_for_testing\\post_build_copy_addins.cmd'
                    }
                }
            }
        }

        stage('Test') {
            steps {
                node ('MA-JENKINS52') {
                    ws('%BUILD_TAG%') {
                        // Run test
                        echo 'Running Unit tests'
                        bat '%WORKSPACE%\\arcgis10_mapping_tools\\run-unittests.cmd'
                    }
                }
            }
        }
    }
    post {
        always {
            node ('MA-JENKINS52') {
                ws('%BUILD_TAG%') {
                    echo 'This will always run'
                    // Archive:
                    archive '/arcgis10_mapping_tools/arcaddins_for_testing/*.esriAddin'
                    junit 'TestResult.xml'
                    // deleteDir() /* clean up our workspace */
                }
            }
        }
        success {
            node ('MA-JENKINS52') {
                ws('%BUILD_TAG%') {
                    echo 'This will run only if successful'
                    bat '"C:\\Program Files (x86)\\Git\\bin\\curl.exe" -XPOST -H "Authorization: token github_mapaction_jenkins" https://api.github.com/repos/mapaction/mapaction-toolbox/statuses/%GIT_COMMIT% -d "{ \\"state\\": \\"success\\", \\"target_url\\": \\"%BUILD_URL%\\", \\"description\\": \\"JENKINS: All tests passed.\\" }"'
                }
            }
        }
        failure {
            node ('MA-JENKINS52') {
                ws('%BUILD_TAG%') {
                    echo 'This will run only if failed'
                    bat '"C:\\Program Files (x86)\\Git\\bin\\curl.exe" -XPOST -H "Authorization: token github_mapaction_jenkins" https://api.github.com/repos/mapaction/mapaction-toolbox/statuses/%GIT_COMMIT% -d "{ \\"state\\": \\"failure\\", \\"target_url\\":  \\"%BUILD_URL%\\", \\"description\\": \\"JENKINS: One or more tests failed.\\" }"'
                }
            }
        }
    }
}