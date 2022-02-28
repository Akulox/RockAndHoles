# Пазл HexGame

Данное описание сделанно для понимания того, что конкретно я умею делать и каких целей мне удалось достичь в работе над этой игрой.

0. [О проекте](#О-проекте)
1. [Руководство пользования](#Руководство-пользования)
2. [План](#План)
3. [Идея](#Идея)
4. [3D Моделирование](#3D-Моделирование)
5. [Реализация](#Реализация)
6. [Итоги разработки](#Итоги-разработки)
7. [Ошибки](#Ошибки)

## О проекте 
Это мой первый проект на Unity и по началу я думал (как типичный новичок), что сделать его сразу +- серьёзным было бы хорошей идеей.  
Перед этим я немного "игрался" с Javascript'ом, пытаясь воссоздать сами головоломки из игры The Witness.  
После этого в голову пришла идея создать какую-то свою уникальную головоломку.  
    
## Руководство пользования
Есть готовая сборка для Android. Её можно просто установить и запустить приложение.  
Чтобы удачно импортировать проект нужна версия Unity 2019.4.30f1. На новых релизах могут возникнуть проблемы с компиляцией.  
После скачивания проекта поменять платформу на Android соответствено.  
Основной сценой является "ManagerScene", находящаяся в папке Scenes.  
Нюансов импорта не знаю, но предварительно проверил, переустановив проект.
    
## Идея
Концепт вот в чём: один уровень состоит из двумерного поля из шестиугольников, игрок может переместить разом все объекты на этих шестиугольниках в любую из шести сторон.
Те объекты, которые выходят за край, переносятся на другую сторону той линии, вдоль которой были перемещены. Используя эту механику игрок должен был соблюсти ряд условий,
чтобы пройти уровень, например, перекрыть всеми объектами "дыры" на поле.
Изначально хотелось сделать дизайн что-то типо настольной голволомки. Но позже появился концепт магического мира, которому угражает зло из под земли, остановить которое нужно с помощью специальных булыжников.                                                                                                                                                                                                                        
## План
Чёткого плана разумеется не было, поскольку я даже не знал, какие этапы меня ждут. Была какая-то база в виде [идея - код - тест - графика - тест - повторить], но не более.  
Работа над проектом велась в одиночку. Я поставил себе цель разобрать основные аспекты каждого этапа, поэтому я не использовал никаких ассетов, и старался по большей части писать код самостоятельно.

## 3D-Моделирование
Я выбрал 3D, потому-что считал его более привлекательным в перспективе.  
Чтобы упростить себе задачу, выбрал "Low Poly" дизайн, тоесть простой. Начав разбираться в Blender'е, создал несколько ассетов: деревья трёх типов, траву, камни, трещины, карту уровней и персонажа.  
Также я анимировал часть ассетов: покачивание у деревьев, травы и несколько движений у персонажа. Детально эти ассеты не текстурировал, тем более не затрагивал шейдеры.  

## Реализация
Я любитель делать всё самостоятельно, не пользовался готовыми ассетами и редко когда приходилось "Ctrl + C Ctrl + V", поэтому проект сделан с нуля.
### Карта уровней  
Реализовать выбор уровней я решил окольным путём: сделать трёхмерную карту уровней. Для этого я сделал модель самой карты, разместил объекты для выбора уровней (при нажатии на которые выскакивает диалоговое окно с описанием уровня), и перемещение  по ней при помощи свайпов.    
### UI 
Его я решил сделать простым. Сделав несколько кнопок в Figma, разместил их на чёрной полупрозрачной панели и в других местах. Так в начале в верхнем углу есть кнопка настроек, которая открывает настройки звука/музыки (вкл/выкл), меню локализации, кнопка "Премиум-версии" и кнопка выхода.
При входе в сам уровень появляется ещё две кнопки: рестарт уровня и выход из уровня.    
### Система диалогов  
Было сделанно несколько вариантов: вопрос (с ответом да/нет), диалог (где персонаж обращается к игроку) и примечание (где игроку даётся небольшая подсказка).    
### Механика уровней  
Для управления уровнем был создан отдельный класс, в котором содержится игровое поле и объекты на поле (tiles и dices).  
Хранятся они в словаре и вызываются строчными индексами. Мне этот метот кажется удобным, хоть на деле может показаться непрактичным.  
Для размещения в редакторе был добавлен простой класс, который добавлял в меню объекта кнопку "Поставить на место". Наверное можно было использовать шестиугольный TileMap, но я не разобрался как. 
### Хранение данных
Хранение данных идёт в Streaming Assets в зашифрованном виде.  
### Локализация  
Локализацию я ришел сделать путём тегов, которые хранились отдельных JSON файлах. Затем в каждом текстовом окне указывался соответствующий тег, а уже в игре текстовое поле заполнялось в соответствии с выбранным языком.    
### Мультисцены  
Посмотрев, что моя игра по сути имеет один UI на протяжении всей игры, мне стало как-то не по себе от его переиспользования, поэтому я сделал основную сцену со всеми основными менеджерами (менеджер сцен, диалогов, данных и пр.)  
Я не знаю наверняка, как оценивается эта практика у других разработчиков, но я сделал так.  

## Итоги разработки
На этот проект в целом ушло 2 месяца.  
За это время я также почти с нуля закрепил базовый уровень C#.   
По итогу я не смог затронуть некоторые основы разработки на Unity: работа с Asset Store, добавление аудио, работа с шейдерами, пост-процессинг, монетизация, детальный левел-дизайн, работа с физикой.  

## Ошибки
Первая ошибка - наивность. Я наивно полагал, что "с первого раза" может получится что-то стоящее. Это разумеется не так, и будет ещё много неполучившихся проектов, прежде чем получится нечто большее.  
Вторая ошибка - отсутствие "коуча". Самообразование вещь конечно хорошая, но лучше, когда тебя кто-то контролирует или так скажем "берёт под своё крыло".  
Третья ошибка - недостаточная организованность.  
