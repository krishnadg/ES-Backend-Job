stage ('Test') {
	podTemplate(
		label: 'dotnet-core-pod',
		containers: [
			containerTemplate(
				name: 'dotnet-core',
				image: 'microsoft/dotnet:1.1.2-sdk',
				ttyEnabled: true,
				command: 'cat',
				
			)
		]
	) {
		node('dotnet-core-pod') {
			container('dotnet-core') {

				checkout scm
				sh 'dotnet restore && dotnet test ESTests/ESTests.csproj --filter Category!=Integration'
			}
		}
	}
}

stage ('Dind') {
  podTemplate(
    label: 'default',
  ) {
    node('default') {
      container('default') {
        git url: 'https://github.com/krishnadg/ES-Backend-Job.git', branch: 'master'
        sh '$(aws ecr get-login --no-include-email --region us-west-2)'
        sh 'docker build -f Dockerfile -t es-backend-job:latest .'
        sh 'docker tag es-backend-job:latest 543369334115.dkr.ecr.us-west-2.amazonaws.com/es-backend-job:latest'
        sh 'docker push 543369334115.dkr.ecr.us-west-2.amazonaws.com/es-backend-job:latest'
      }
    }
  }
}