pipeline {
  agent any

  options {
    timestamps()
    // Hvis ansiColor driller igen, så fjern næste linje eller installer "AnsiColor" plugin
    ansiColor('xterm')
    disableConcurrentBuilds()
  }

  environment {
    DOTNET_IMAGE = 'mcr.microsoft.com/dotnet/sdk:8.0'
    API_CSPROJ   = 'FitSammen/FitSammen_API/FitSammen_API.csproj'
    API_IMAGE    = 'fitsammen-api:ci'
    DOCKERFILE   = 'Dockerfile'
  }

  stages {

    stage('Checkout') {
      steps {
        checkout scm
      }
    }

    stage('Build & Test (API only)') {
      steps {
        script {
          docker.image(env.DOTNET_IMAGE).inside('-u root:root') {
            sh '''#!/usr/bin/env bash
              set -euo pipefail

              echo "=== Repo root ==="
              pwd
              ls -la

              echo "=== .NET version ==="
              dotnet --version

              echo "=== Validate API csproj path ==="
              if [[ ! -f "${API_CSPROJ}" ]]; then
                echo "ERROR: Could not find API csproj at: ${API_CSPROJ}"
                echo "TIP: Check your repo structure or update API_CSPROJ in Jenkinsfile"
                exit 1
              fi
              echo "Using csproj: ${API_CSPROJ}"

              echo "=== Restore ==="
              dotnet restore "${API_CSPROJ}"

              echo "=== Build ==="
              dotnet build "${API_CSPROJ}" -c Release --no-restore

              echo "=== Test (API only) ==="
              dotnet test "${API_CSPROJ}" -c Release --no-build
            '''
          }
        }
      }
    }

    stage('Docker Build (API image)') {
      steps {
        sh '''#!/usr/bin/env bash
          set -euo pipefail

          echo "=== Docker version ==="
          docker --version

          echo "=== Validate Dockerfile ==="
          if [[ ! -f "${DOCKERFILE}" ]]; then
            echo "ERROR: Dockerfile not found at repo root: ${DOCKERFILE}"
            echo "TIP: Commit Dockerfile to the repo root (same place as Jenkinsfile), or update DOCKERFILE in Jenkinsfile"
            exit 1
          fi

          echo "=== Docker build: ${API_IMAGE} ==="
          docker build -t "${API_IMAGE}" -f "${DOCKERFILE}" .
        '''
      }
    }
  }

  post {
    success {
      echo '✅ Pipeline success'
    }
    failure {
      echo '❌ Pipeline fejlede – se Console Output'
    }
    always {
      cleanWs()
    }
  }
}