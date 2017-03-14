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
        stage('Build') {
            steps {
                echo 'Building'
                git(url: 'https://github.com/mapaction/mapaction-toolbox.git', poll: true)
                bat '""${tool \'MSBuild\'}" arcgis10_mapping_tools/MapAction-toolbox.sln /t:build /p:PlatformTarget=x86 /p:Configuration=Release /maxcpucount /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}'
                bat '$env.WORKSPACE\arcgis10_mapping_tools\arcaddins_for_testing\post_build_copy_addins.cmd'
            }
        }
        stage('Test') {
            steps {
                echo 'testing'
                bat '$env.WORKSPACE\arcgis10_mapping_tools\run-unittests.cmd'
                archiveArtifacts 'arcgis10_mapping_tools/arcaddins_for_testing/*.esriAddin'
                junit 'TestResult.xml'
            }
        }
        stage('Deploy') {
            steps {
                echo 'deploying'
            }
        }
    }
}