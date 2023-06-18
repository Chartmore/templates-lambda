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
3. From the root of this repository, run `sam build`
4. Once complete, run the integration tests from rider

## Still to do:
1. Correctly call lambda from integration tests so that errors within the function bubble up to the test
2. Configure all this to run in CI pipeline
3. We got so close and had it mostly working and actually calling the function but now it seems when i start-lambda it cannot invoke the function any more?