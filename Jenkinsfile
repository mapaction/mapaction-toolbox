#!/usr/bin/env groovy

node {
    echo "${env.JOB_NAME}"
    echo "${env.BUILD_NUMBER}"
    echo "${env.JOB_BASE_NAME}"
    echo "${env.BUILD_TAG}"
    ws ("workspace\\mapaction-toolbox\\${env.BUILD_NUMBER}") {
        
        timeout(time: 10, unit: 'MINUTES'){
            //disableConcurrentBuilds()
            // timestamps()

            /*
            triggers {
                // nightly build at ~0500hrs
                cron('H H(4-6) * * *')
                // pollSCM every ten minuites
                pollSCM('H/10 * * * *')
            }
            */

            stage('Build') {
                echo 'Building'
                checkout scm
                // tool name: 'MSBuild v4.0.30319', type: 'msbuild'
                bat "\"${tool 'MSBuild v4.0.30319'}\" arcgis10_mapping_tools/MapAction-toolbox.sln /t:build /p:PlatformTarget=x86 /p:Configuration=Release /maxcpucount"
                bat '${env.WORKSPACE}\\arcgis10_mapping_tools\\arcaddins_for_testing\\post_build_copy_addins.cmd'
            }
            stage('Test') {
                echo 'testing'
                bat '$env.WORKSPACE\\arcgis10_mapping_tools\\run-unittests.cmd'
                archiveArtifacts 'arcgis10_mapping_tools/arcaddins_for_testing/*.esriAddin'
                junit 'TestResult.xml'
            }
            stage('Deploy') {
                echo 'deploying'
            }
        }
    }
}