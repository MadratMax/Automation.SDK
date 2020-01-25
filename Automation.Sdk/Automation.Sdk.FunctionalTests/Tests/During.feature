Feature: During

Scenario: During Tests
	When some step during 30 seconds
	And some step with table during 30 seconds
		| Table |
		| Value |
	Then some step during 30 seconds
	And some step with table during 30 seconds
		| Table |
		| Value |