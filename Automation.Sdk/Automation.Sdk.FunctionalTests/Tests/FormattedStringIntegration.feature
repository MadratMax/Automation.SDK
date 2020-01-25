Feature: FormattedStringIntegration

@BindingsIntegrity	
Scenario: formatted string should be transformed implicitly
	Given string "test" should be converted to formatted string

@BindingsIntegrity	
Scenario: formatted string value should not be changed if there is no placeholders
	Given formatted string "test" value should be "test"

@BindingsIntegrity	
Scenario: placeholders should be replaced in formatted string
	Given formatted string "<space>te<space>st<space>" value should be " te st "