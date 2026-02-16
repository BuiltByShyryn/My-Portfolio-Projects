from django.contrib import admin
from .models import Author, Publisher, Book, Publication, Sales

admin.site.register(Author)
admin.site.register(Publisher)
admin.site.register(Book)
admin.site.register(Publication)
admin.site.register(Sales)