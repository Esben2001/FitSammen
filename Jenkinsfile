pipeline {
  agent none

  stages {
    stage('Build & Test (.NET SDK)') {
      agent {
        docker {
          image 'mcr.microsoft.com/dotnet/sdk:8.0'
          args '-u root:root'
        }
      }
      steps {
        sh 'dotnet --version'
        sh 'dotnet build FitSammen/FitSammen/FitSammen_API/FitSammen_API.csproj -c Release'
        sh 'dotnet test -c Release'
      }
    }

    stage('Docker Build') {
      agent any
      steps {
        sh 'docker --version'
        sh 'docker build -t fitsammen-api:ci .'
      }
    }
  }
}