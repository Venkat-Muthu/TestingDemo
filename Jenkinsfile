pipeline {
    options {
        buildDiscarder(logRotator(daysToKeepStr: '20', numToKeepStr: '12'))
        timeout(time: 60, unit: 'MINUTES')
    }

    agent {
        node {
            label 'Master'
            customWorkspace "c:/jenkins/agent/workspace/TestingDemo/${BRANCH_NAME}"
        }
    }

    environment {
        scannerHome = tool 'SonarQube Scanner'
		sqScannerMsBuildHome = tool 'Scanner for MSBuild 4.7'
    }

    stages{
		stage('PR - Build + PR Analysis'){
			when{
                expression{
                    return BRANCH_NAME.matches("PR.*")
                }
            }
            steps{
				script {
					currentBuild.displayName = "#${BUILD_NUMBER} - ${BRANCH_NAME}"
				}
				bat 'call powershell.exe Build.TestingDemo.ps1'
            }
			post{
                always{
				
					nunit testResultsPattern: '**/nunit-result.xml'
                }
                success{
					archiveArtifacts 'WindowsFormUI/bin/Release/**'
                }
            }
		}

	    stage('Master / Release - Build + Static Code Analysis'){
		    when{
                expression{
                    return (BRANCH_NAME.matches("master") || BRANCH_NAME.matches("release/.*"))
                }
            }
            steps{
				script {
					currentBuild.displayName = "#${BUILD_NUMBER} - ${BRANCH_NAME}"
				}
				bat 'call powershell.exe Build.TestingDemo.ps1'
            }
			post{
                always{
					nunit testResultsPattern: '**/nunit-result.xml'
                }
                success{
                    archiveArtifacts 'WindowsFormUI/bin/Release/**'
                }
            }
        }
    }
}
