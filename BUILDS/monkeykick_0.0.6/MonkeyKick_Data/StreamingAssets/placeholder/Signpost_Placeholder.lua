local main = function()
	
	ToggleChoosingChoice(false)
	State.SetFlag("Read", false)
	
	SetCharacterName("Signpost")	
	SetText("HELLO CREATURE. TIME TO TELL YOU THE CONTROLS OF THIS DEMO.")
	coroutine.yield()
	SetText("OVERWORLD CONTROLS: USE THE ARROW KEYS TO MOVE ON KEYBOARD, OR THE LEFT-STICK ON CONTROLLER. USE SPACE TO JUMP ON KEYBOARD, OR THE A (XBOX) OR X (PLAYSTATION) BUTTON.")
	coroutine.yield()
	SetText("USE SHIFT TO SPRINT ON KEYBOARD, OR B (XBOX) OR CIRCLE (PLAYSTATION) ON CONTROLLER. USE E TO INTERRACT ON KEYBOARD, OR THE Y (XBOX) OR TRIANGLE (PLAYSTATION) ON CONTROLLER.")
	coroutine.yield()
	SetText("USE TAB TO OPEN THE MENU ON KEYBOARD, OR THE START (XBOX) OR OPTIONS (PLAYSTATION) BUTTON TO OPEN THE MENU ON CONTROLLER.")
	coroutine.yield()	
	SetText("BATTLE CONTROLS: USE THE ARROW KEYS TO CHOOSE DIFFERENT OPTIONS ON THE BATTLE MENU, OR LEFT-STICK ON CONTROLLER. USE THE SPACE BAR TO SELECT YOUR OPTION ON KEYBOARD, OR THE A (XBOX) OR X (PLAYSTATION) ON CONTROLLER.")
	coroutine.yield()
	SetText("USE THE BUTTON PROMPTS ON SCREEN TO USE YOUR ATTACKS.")
	coroutine.yield()
	
	SetCharacterName("P-Dawg")
	SetText("Oh, there's a message on the bottom of the sign...")
	coroutine.yield()
	
	SetCharacterName("")
	SetText("Read the new message at the bottom of the sign?")
	ShowButtons("Keep Reading", "Leave")
	ToggleChoosingChoice(true)
	coroutine.yield()
	
	if State.ChoiceSelected == 1 then
		ToggleChoosingChoice(false)
		SetCharacterName("Signpost")
		SetText("P.S. DOIN UR MOM DOIN DOIN UR MOM. ~Signpost Writer")
		State.SetFlag("Read", true)
		coroutine.yield()
	else
		ToggleChoosingChoice(false)
	end
	
	if State.GetFlag("Read") then
		SetCharacterName("P-Dawg")
		SetText("Fuck off.")
	end
	
end

return main