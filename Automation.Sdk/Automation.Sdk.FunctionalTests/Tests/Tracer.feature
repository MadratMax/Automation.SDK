Feature: Tracer

@TracerTest
Scenario: Trace message
    Given Step 1 "Trace test message"
        When trace messge "exception test"
        And trace messge "exception test2"
    Given Step 2 "Trace more test messages"
        When trace messge "test3"
        And trace messge "exception test4"
    Given Step 3 "Check messages"
        Then log should contain the following messages:
        | message                  |
        | PRODUCT: exception test  |
        | PRODUCT: exception test2 |
        | PRODUCT: exception test4 |