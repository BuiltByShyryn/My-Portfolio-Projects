from django.db import models
#Экзаменационное задание N 02
#Таблицы Пояснения 
#Authors Авторы (ID, ФИО, год рождения и др.) 
#Books Книги (ID, название книги, ID-автора, жанр, год написания, и др.) 
#Publishers Издатели (ID, название издательства) 
#Publications Публикации (издательство, книга, дата, тираж, и т.д. ) 
#Sales Годовые продажи (публикация, год, цена одной книги, количество проданных 
#книг, общая стоимость проданных книг и пр.)
# Create your models here.
class Author(models.Model):
    full_name = models.CharField(max_length=250)
    birth_year = models.IntegerField()
    
    def __str__(self):
        return self.full_name
    
class Publisher(models.Model):
    name = models.CharField(max_length=255)
    
    def __str__(self):
        return self.name
    
class Book(models.Model):
    title = models.CharField(max_length=255)
    author = models.ForeignKey(Author,on_delete=models.CASCADE)
    genre = models.CharField(max_length=100)
    writing_year = models.IntegerField()
    
    def __str__(self):
        return self.title
    
class Publication(models.Model): 
    publisher = models.ForeignKey(Publisher, on_delete=models.CASCADE)
    book = models.ForeignKey(Book, on_delete=models.CASCADE)
    date = models.DateField()
    circulation = models.IntegerField()
    def __str__(self):
        return f"{self.book.title} published by {self.publisher.name}" 
class Sales(models.Model):
    publication = models.ForeignKey(Publication,on_delete=models.CASCADE)
    year = models.IntegerField()
    quantity_sold = models.IntegerField()
    total_cost = models.DecimalField(max_digits=15, decimal_places=2)
    def __str__(self):
        return f"{self.publication.book.title} - Sales for {self.year}"