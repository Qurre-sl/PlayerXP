# PlayerXP
##### Motivate players to play on your server with levels
## Installation
* Move `PlayerXP.dll` into the `~/.config`(`%AppData%` on Windows)`/Qurre/Plugins`

Config:
```yml
#needed for translation (ex: 1 level, 2 level, etc)
playerxp_lvl: level
#If a player writes in his nickname "# + the name of your project", he will receive 2 times more experience
playerxp_project: fydne
#BroadCast to the player when he joins the server.
#fydne - # + the \"playerxp_project\" you specified
playerxp_join: <color=red>If you write in the nickname</color> <color=#9bff00>#fydne</color>,\n<color=#fdffbb>then you will get 2 times more experience</color>
#BroadCast to a player when they level up
playerxp_lvl_up: <color=#fdffbb>You got %lvl% level! Until the next level you are missing %to.xp% xp.</color>
#BroadCast to a player when they get xp for escaping
playerxp_escape: <color=#fdffbb>You got %xp%xp for escaping</color>
#BroadCast to a player when they get xp for killing a player
playerxp_kill: <color=#fdffbb>You got %xp%xp for killing</color> <color=red>%player%</color>
#Level prefixes
#Example: (playerxp_prefixs: 1:beginner,2:player,3:thinking) - (1 level | beginner ; 2 level | player ; 3 level | thinking ; etc..
playerxp_prefixs: 
```
```yaml
#RU
#Нужен для более корректного перевода. Пример отображения: 1 уровень, 2 уровень, 3 уровень и т.д
playerxp_lvl: уровень
#Название вашего проекта.
#Если игроки напишут в своем нике # + название вашего проекта, они будут получать в 2 раза больше опыта
playerxp_project: fydne
#BroadCast игроку при заходе на сервер, что если он напишет в нике # + название вашего проекта, он будет получать в 2 раза больше опыта
playerxp_join: <color=red>Если вы напишите в нике</color> <color=#9bff00>#fydne</color>,\n<color=#fdffbb>то вы будете получать в 2 раза больше опыта</color>
#BroadCast игроку о повышении уровня
playerxp_lvl_up: <color=#fdffbb>Вы получили %lvl% уровень! До следующего уровня вам не хватает %to.xp% xp.</color>
#BroadCast игроку о получении xp за побег
playerxp_escape: <color=#fdffbb>Вы получили %xp%xp за побег</color>
#BroadCast игроку о получении xp за убийство другого игрока
playerxp_kill: <color=#fdffbb>Вы получили %xp%xp за убийство</color> <color=red>%player%</color>
#Префиксы уровней
#Пример: (playerxp_prefixs: 1:новичок,2:игрок,3:thinking) - (1 уровень | новичок ; 2 уровень | игрок ; 3 level | thinking ; и т.д)
playerxp_prefixs: 
```