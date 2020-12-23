local main = function()

	ToggleChoosingChoice(false)
	State.SetFlag("Read", false)
	
	SetCharacterName("Signpost")
	SetText("THIS IS THE OVERWORLD MOVEMENT TEST AREA. IT IS UNDER CONSTRUCTION. HERE ARE THE CONTROLS.")
	coroutine.yield()
	SetText("(KEYBOARD) ARROW KEYS / (CONTROLLER) LEFT JOYSTICK IS USED TO WALK. (KEYBOARD) SPACE / (XBOX) A / (PS4) X IS USED TO JUMP. (KEYBOARD) SHIFT / (XBOX) B / (PS4) CIRCLE IS USED TO SPRINT.")
	coroutine.yield()
	SetText("THAT'S IT FOR NOW, LATER MORE ACROBATIC ACTIONS AND OVERWORLD ABILITIES WILL BE USED.")
	coroutine.yield()
	
	SetCharacterName("Merle")
	SetText("I can't wait.")
	coroutine.yield()
	
end

return main