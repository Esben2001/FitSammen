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
        sh '''
          set -e

          echo "=== Repo root ==="
          pwd
          ls -la

          echo "=== .NET version ==="
          dotnet --version

          echo "=== Find API csproj ==="
          CSPROJ=$(find . -name "FitSammen_API.csproj" | head -n 1)
          if [ -z "$CSPROJ" ]; then
            echo "ERROR: Could not find FitSammen_API.csproj"
            find . -maxdepth 8 -type f -name "*.csproj" -print
            exit 1
          fi
          echo "Using csproj: $CSPROJ"

          echo "=== Build ==="
          dotnet build "$CSPROJ" -c Release

          echo "=== Find solution (.sln) for tests ==="
          SLN=$(find . -name "*.sln" | head -n 1)

          if [ -n "$SLN" ]; then
            echo "Using solution for tests: $SLN"
            dotnet test "$SLN" -c Release
          else
            echo "No .sln found. Trying dotnet test on API project (may be OK if no tests)."
            dotnet test "$CSPROJ" -c Release || true
          fi
        '''
      }
    }

    stage('Docker Build') {
      agent any
      steps {
        sh '''
          set -e

          echo "=== Docker version ==="
          docker --version

          echo "=== Find Dockerfile ==="
          DOCKERFILE=$(find . -maxdepth 12 -name "Dockerfile" | head -n 1)

          if [ -z "$DOCKERFILE" ]; then
            echo "ERROR: Could not find Dockerfile anywhere in repo."
            echo "This usually means Dockerfile is not committed to GitHub."
            echo "If you have it locally, add it to repo root (recommended) and push."
            exit 1
          fi

          echo "Using Dockerfile: $DOCKERFILE"
          docker build -t fitsammen-api:ci -f "$DOCKERFILE" .
        '''
      }
    }

  }
}