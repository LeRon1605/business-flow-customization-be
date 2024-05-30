#!/bin/bash

echo y | docker system prune

# docker build -t rubeha/business_flow_customization_api_gateway -f ApiGateway/Dockerfile .
# docker build -t rubeha/business_flow_api:latest -f Services/BusinessFlow/Presentation/BusinessFlow.Api/Dockerfile .
# docker build -t rubeha/hub_api:latest -f Services/Hub/Presentation/Hub.Api/Dockerfile .
# docker build -t rubeha/identity_api:latest -f Services/Identity/Presentation/Identity.Api/Dockerfile .
# docker build -t rubeha/submission_api:latest -f Services/Submission/Presentation/Submission.Api/Dockerfile .

# docker push rubeha/business_flow_customization_api_gateway:latest
# docker push rubeha/business_flow_api:latest
# docker push rubeha/hub_api:latest
# docker push rubeha/identity_api:latest
# docker push rubeha/submission_api:latest