pipeline {
  agent any

  options {
    timestamps()
    ansiColor('xterm')
    disableConcurrentBuilds()
  }

  environment {
    DOTNET_IMAGE = 'mcr.microsoft.com/dotnet/sdk:8.0'
    API_DIR      = 'FitSammen/FitSammen_API'
    CSPROJ       = "${API_DIR}/FitSammen_API.csproj"
    DOCKER_TAG   = 'fitsammen-api:ci'
    DOCKERFILE   = "${API_DIR}/Dockerfile"
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
            sh """
              set -euo pipefail
              echo "=== .NET version ==="
              dotnet --version

              echo "=== Restore ==="
              dotnet restore "${CSPROJ}"

              echo "=== Build ==="
              dotnet build "${CSPROJ}" -c Release --no-restore

              echo "=== Test ==="
              dotnet test "${CSPROJ}" -c Release --no-build
            """
          }
        }
      }
    }

    stage('Docker Build (API image)') {
      steps {
        sh """
          set -euo pipefail
          echo "=== Docker version ==="
          docker --version

          echo "=== Docker build ==="
          docker build -t "${DOCKER_TAG}" -f "${DOCKERFILE}" "${API_DIR}"
        """
      }
    }
  }

  post {
    success {
      echo "✅ Pipeline OK: build + test + docker image lavet: ${DOCKER_TAG}"
    }
    failure {
      echo "❌ Pipeline fejlede – se Console Output"
    }
    always {
      cleanWs(deleteDirs: true)
    }
  }
}