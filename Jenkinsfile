pipeline {
  agent any

  stages {

    stage('Checkout') {
      steps {
        checkout scm
      }
    }

    stage('Build') {
      steps {
        sh 'dotnet build FitSammen/FitSammen/FitSammen_API/FitSammen_API.csproj -c Release'
      }
    }

    stage('Test') {
      steps {
        sh 'dotnet test'
      }
    }

    stage('Docker Build') {
      steps {
        sh 'docker build -t fitsammen-api:ci .'
      }
    }

  }
}