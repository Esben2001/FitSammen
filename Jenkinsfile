pipeline {
  agent any

  stages {

    stage('Checkout') {
      steps {
        checkout scm
      }
    }

    stage('Build & Test (API only, .NET in Docker)') {
      steps {
        script {
          docker.image('mcr.microsoft.com/dotnet/sdk:8.0').inside('-u root:root') {
            sh '''
              set -e
              echo "=== Repo root ==="
              pwd
              ls -la

              echo "=== .NET version ==="
              dotnet --version

              echo "=== Find API csproj ==="
              CSPROJ=$(find . -name FitSammen_API.csproj | head -n 1)
              if [ -z "$CSPROJ" ]; then
                echo "ERROR: Could not find FitSammen_API.csproj"
                exit 1
              fi
              echo "Using csproj: $CSPROJ"

              echo "=== Restore & Build ==="
              dotnet restore "$CSPROJ"
              dotnet build "$CSPROJ" -c Release --no-restore

              echo "=== Test (API only) ==="
              dotnet test "$CSPROJ" -c Release --no-build
            '''
          }
        }
      }
    }

    stage('Docker Build (API image)') {
      steps {
        sh '''
          set -e
          echo "=== Docker version ==="
          docker --version

          echo "=== Find Dockerfile ==="
          DOCKERFILE=$(find . -name Dockerfile | head -n 1)
          if [ -z "$DOCKERFILE" ]; then
            echo "ERROR: Could not find Dockerfile in repo. Commit/push it to GitHub."
            exit 1
          fi
          echo "Using Dockerfile: $DOCKERFILE"

          echo "=== Docker build ==="
          docker build -t fitsammen-api:ci -f "$DOCKERFILE" .
        '''
      }
    }

  }
}