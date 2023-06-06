# lambda-template

## Run Locally

1. Install AWS [SAM CLI](https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/install-sam-cli.html)
2. Ensure docker is running locally
3. From the root of this repository, run `sam build`
4. Once complete, run `sam local invoke`

Note: Local invocations use the event json from `./events/event.json`, so update that to update the trigger for the function

## Integration Test

1. Install AWS [SAM CLI](https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/install-sam-cli.html)
2. Ensure docker is running locally
3. From the repository root, run `sam local start-lambda`
4. Once complete, run the integration tests from rider