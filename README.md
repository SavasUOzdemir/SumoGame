# SumoGame
 
##EN
This is the case study that I have completed for a job application of mine. The whole project lasted about 13 hours to make from start to finish. As per the requirements of the case, I also added touch controls but this was the first time I did that and my mobile device is too old for Unity Remote. So, god only knows whether touch functionality works. 

I have used free asset from Unity Store, Game GUI Vol1 for pause and resume buttons. Below are some screenshots from the game. 

##TR

Bu, bir iş başvurum için tamamladığım vaka çalışması. Tüm proje baştan sona yaklaşık 13 saat sürdü. Vakanın gerekliliklerine göre dokunmatik kontroller de ekledim ama bunu ilk kez yaptım ve mobil cihazım Unity Remote için çok eski. Yani, dokunma işlevinin çalışıp çalışmadığını yalnızca tanrı bilir.

Duraklatma ve devam ettirme düğmeleri için Unity Store, Game GUI Vol1'den ücretsiz varlık kullandım. Aşağıda oyundan bazı ekran görüntüleri var.


Game Start - Başlangıç Ekranı
![image](https://user-images.githubusercontent.com/94976406/216799541-7605c0fd-eeb0-4b63-9757-e6d494a8a9a5.png)

After Instantiating food and AI agents || Besin ve AI oyuncular başlatıldıktan sonra
![image](https://user-images.githubusercontent.com/94976406/216799550-48b430f3-8d20-4545-ad5e-50dfad1a521d.png)

After Player-controlled character falls-dies. || karakterimiz düşüp öldükten sonra
![image](https://user-images.githubusercontent.com/94976406/216799580-f67763ea-b96a-4e1b-a936-3f39606da4fc.png)

Game Over screen || Oyun Bitti ekranı
![image](https://user-images.githubusercontent.com/94976406/216799604-95d02b73-6079-4235-bfd3-8bf0ee4d831f.png)


05.02.2023, 22:07TRT - Known Issues
-If player collides with other multiple times before killing them, score is calculated multiple times. This can be avoided by adding a method to remember the name of the collision objects and apply score once once one falls down. Another potential solution may be to keep this information in a separate empty gameobject.
-Even though playmode starts without any compile errors, after food items are consumed / destroyed, the AI agents throw nullreferenceexception errors. This may be solved again by keeping the list of active food and player gameobjects in a separate list, or adding nullchecks to their related behavior to re-calculate on targets being null. However, in the editor this does not really cause any actual problems.
