VAR CharacterName = "Стрелок"

Карамба
    +[На абордаж]
        ->continue
    +[Самолет мне в ангар]
        ->continue
        
=== continue ===
Вы никто, и звать Вас никак!!!
    +[Тротил мне в задницу]
        ->answer
    +[ЭЭЭ, АХРИНЕЛ]
        ->answer2
->END
        
=== answer ===
Окей
~CharacterName  = "Негр"
Джамбо
->END

=== answer2 ===
Окей
~CharacterName  = "Еврей"
Я Еврей, и я в ШТОРМ Z!!!
->END