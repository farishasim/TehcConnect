# TehcConnect

TehcConnect adalah sebuah aplikasi desktop sederhana yang dapat memodelkan beberapa fitur dari People You May Know dalam jejaring sosial media (Social Network).

Dengan memanfaatkan algoritma Breadth First Search (BFS) dan Depth First Search (DFS), aplikasi ini menyediakan dua buah fitur utama yaitu
- Fitur pertama adalah fitur rekomendasi teman yaitu menampilkan daftar rekomendasi teman untuk suatu akun yang ditentukan berdasarkan mutual friend yang dimiliki dan diurutkan mulai dari mutual friend terbanyak.  
- Fitur kedua adalah fitur eksplorasi teman dimana dua akun yang belum berteman dan tidak memiliki mutual friends sama sekali bisa berkenalan melalui jalur tertentu. 

## Instalasi
Aplikasi ini dapat dijalankan di Windows. Untuk melakukan instalasi program lakukan langkah-langkah berikut
- clone repositori ini
```
git clone https://github.com/almeizaarvin/TubesGraph.git
```
- Untuk menjalankan aplikasi masuk ke direktori `bin > release > netcoreapp3.1`,
kemudian jalankan TubesGraph.exe, dengan cara klik dua kali.

## Cara Pemakaian

![Running App](/Properties/Run.gif)

Untuk menggunakan aplikasi ini, 
- Jalankan terlebih dahulu aplikasi ini, kemudian 
- Tekan tombol `Open File` untuk memasukkan file yang berisi graf beserta edge nya. Beberapa file yang siap pakai sudah disediakan pada folder `test`

Untuk menggunakan fitur rekomendasi teman :
- Pilih salah satu akun yang ingin diketahui daftar rekomendasi temannya.

Untuk menggunakan fitur eksplorasi teman :
- Pilih dua buah akun yang akan ditelusuri jalur pertemanannya.
- Pilih algoritma yang akan digunakan untuk menulusuri jejaring pertemanan ( BFS atau DFS ).
- Tekan tombol submit, visualisasi graf akan ditampilkan dan pencarian akan dimulai.

## Author

[Karlsen Adiyasa Bachtiar - 13519001](https://github.com/karlsenab7)

[Faris Hasim Syauqi - 13519050](https://github.com/farishasim)

[Almeiza Arvin Muzaki - 13519066](https://github.com/almeizaarvin)