pipeline {
	agent any
	stages {
        stage ('Checkout') {
            steps {
                checkout scm
            }
        }
        stage ('Build & UnitTest') {
            steps {
                sh "docker build -t netcore:b${BUILD_NUMBER} -f src/Dockerfile src/"
            }
        }
        stage('Deploy') {
            stages {
                stage ('Deploy Development') {
                    when {
                        expression { BRANCH_NAME == 'develop' }
                        anyOf {
                            environment name: 'DEPLOY_TO', value: 'development'
                        }
                    }
                    steps {
                        echo 'Deploying develop'
                    }
                }
                stage ('Deploy Staging') {
                    when {
                        branch 'staging'
                    }
                    steps {
                        input message: 'Finished deploy to staging? (Click "Proceed" to continue)'
                        echo 'Deploying'
                    }
                }
                stage ('Deploy Production') {
                    when {
                        branch 'master'
                    }
                    steps {
                        input message: 'Finished deploy to production? (Click "Proceed" to continue)'
                        echo 'Deploying'
                    }
                }
            }
        }
    }
}