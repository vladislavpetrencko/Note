1)	Импортировать базу данных в Microsoft SQL Server Management Studio 18 и назвать её note
2)	Скачать Node.js с сайта https://nodejs.org/en/download/
3)	Открыть командную строку и вписать npm install -g iisexpress-proxy
4)	Потом открыть проект GG, открыть Startup и в 21 сорочке вписать название своего SQL сервера
5)	Запустите проект GG
6)	Откроется сайт, запомните порт которой идёт после localhost
7)	Вернитесь к командной строке и впишите команду iisexpress-proxy (порт который был на сайте) to 3000
8)	Скопируйте адрес, которая будет напротив Ethernet
9)	Откройте проект GG2, откройте папку Service и зайдите в класс NoteService
10)	В строчке 13 вставьте свой адрес, чтобы получилось так: “ваш адресс”/api/note
11)	Запустите эмулятор Android 
