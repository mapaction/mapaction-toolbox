#!/usr/bin/env groovy

// require an ArcGIS licence to run tests
node('arcgis102-licence') {
    ws ("workspace\\mapaction-toolbox\\${env.BUILD_NUMBER}") {
        
        /*
        properties{
            PipelineTriggersJobProperty(triggers: [
                PeriodicFolderTrigger(interval: 60),
                SCMTrigger(scmpoll: 15)
            ])
        */  
        
        /*
        properties [$class: 'PipelineTriggersJobProperty',
            triggers: [
                [$class: 'PeriodicFolderTrigger', 
                    interval: '60'
                ],
                [$class: 'SCMTrigger', 
                    scmpoll: '15'
                ],
            ]
        ]
        */
        
        timeout(time: 10, unit: 'MINUTES'){
            
            stage('Build') {
                echo 'Stage: Build'
                checkout scm
                // build sln
                bat "\"${tool 'MSBuild v4.0.30319'}\" arcgis10_mapping_tools/MapAction-toolbox.sln /t:build /p:PlatformTarget=x86 /p:Configuration=Release /maxcpucount"
                // copy the resultant esriAddIns to a common directory
                bat 'arcgis10_mapping_tools\\arcaddins_for_testing\\post_build_copy_addins.cmd'
            }
            stage('Test') {
                echo 'Stage: Test'
                try{
                    bat 'arcgis10_mapping_tools\\run-unittests.cmd'
                } catch (error) {
                    error 'some unittests failed'
                } finally {
                    // publish nUnit results
                    step([$class: 'NUnitPublisher', testResultsPattern: 'TestResult.xml', debug: false, keepJUnitReports: true, skipJUnitArchiver:false, failIfNoResults: true])
                    // archieve the esriAddIns
                    archiveArtifacts 'arcgis10_mapping_tools/arcaddins_for_testing/*.esriAddIn'
                }
            }
            stage('Deploy') {
                echo 'deploying'
            }
        }
    }
}