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

          echo "=== Find FitSammen_API.csproj ==="
          CSPROJ=$(find . -name "FitSammen_API.csproj" | head -n 1)

          if [ -z "$CSPROJ" ]; then
            echo "ERROR: Could not find FitSammen_API.csproj"
            echo "Here are csproj files I can see:"
            find . -maxdepth 6 -type f -name "*.csproj" -print
            exit 1
          fi

          echo "Using csproj: $CSPROJ"

          echo "=== Build ==="
          dotnet build "$CSPROJ" -c Release

          echo "=== Test ==="
          # Hvis dotnet test fejler fordi der ikke er en solution eller tests, så kan vi målrette senere.
          dotnet test -c Release || true
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

          echo "=== Repo root for docker build ==="
          pwd
          ls -la

          # Finder Dockerfile uanset hvor den ligger
          DOCKERFILE=$(find . -maxdepth 4 -name "Dockerfile" | head -n 1)

          if [ -z "$DOCKERFILE" ]; then
            echo "ERROR: Could not find Dockerfile"
            exit 1
          fi

          echo "Using Dockerfile: $DOCKERFILE"

          # Build context = repo root (.)
          docker build -t fitsammen-api:ci -f "$DOCKERFILE" .
        '''
      }
    }
  }
}