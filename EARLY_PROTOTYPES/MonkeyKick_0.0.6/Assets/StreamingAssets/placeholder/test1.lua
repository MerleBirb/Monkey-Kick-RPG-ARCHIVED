local main = function()
	
	SetCharacterName("Signpost")
	SetText("This is a sign. Test, I hope the text works. Wooooo text placeholder text aaaaaa.")
	coroutine.yield()
	SetText("More Placeholder testing TEXT AAAA")
	coroutine.yield()
	
	SetCharacterName("P-Dawg")
	SetText("What the junk, a sign can talk?")
	coroutine.yield()
	
	SetCharacterName("Signpost")
	SetText("Yeah? You got a problem with that?")
	
end

return main