from django.db import models

# Create your models here.
 
class Brand(models.Model):
    name = models.CharField(max_length=100,
                            verbose_name="Brend Name",
                            unique=True,
                            blank=False,
                            null =False,
                            db_index= True,
                            )
    country = models.CharField(max_length=100,
                               verbose_name="Brand's Country")
    
  
    info = models.TextField(verbose_name="Brend info",
                            blank=True,
                            null=True)
   
   
    big_logo = models.ImageField(verbose_name="Logo of The Brand",
                                 upload_to="images",
                                 blank=True,null=True)
    
    small_logo = models.BinaryField(verbose_name="Brand author",
                                  )
    def __str__(self) -> str:
        return f'{self.name}'
    
    class  Meta:
        managed = True
        verbose_name = 'Brand'
        verbose_name_plural = 'Brands'
    pass

class Goods( models.Model):
    #id_goods = models.BigAutoField(primary_key=True,)
    name = models.CharField(max_length=200,
                            verbose_name="Goods Name",
                            db_index=True,
                            blank=False, null=False)
    id_brand = models.ForeignKey( 'Brand', on_delete=models.CASCADE, 
                                 #to_field='id'
                                 verbose_name="Brand Owners "
                                 )
    
    count = models.PositiveIntegerField(verbose_name="Amount")
    price = models.DecimalField(max_digits=16, decimal_places=6, 
                                verbose_name="The Goods Price")
    info = models.TextField(verbose_name="Info about the good", 
                            blank=True,null=True)  
   
    image = models.ImageField(verbose_name="Photo of the picture", 
                              upload_to="images",
                              blank=True, null=True)
    
    
    def __str__(self):
        return f'{self.name} ({self.id_brand})'
    
    class Meta:
        
        managed = True
        verbose_name = 'Good'
        verbose_name_plural = 'Goods'
        unique_together = [("name", 'id_brand'), ]
    
class Customer(models.Model):
    
    fio = models.CharField(max_length=100, 
                           verbose_name="FIO Client",
                           blank=False, null=False)
    
    iin = models.CharField(max_length=12,verbose_name="IIN Client",
                           unique=True, db_index=True,
                           blank=False,null=False,)
    phone = models.CharField(max_length=12, verbose_name="Phone Number", 
                             blank=True,null=True)
    
    email = models.EmailField(max_length=254, verbose_name="Email"
                              )
    
    image = models.BinaryField( null=True, blank=True, editable=True)
    def __str__(self):
        return f"{self.fio}"
    
    
    class Meta:
        
        managed = True
        verbose_name = 'Client'
        verbose_name_plural = 'Clients'
    
    pass

class Sales(models.Model):
    
    id_goods = models.ManyToManyField( 'Goods', related_name='Customer' )
    id_customer = models.ManyToManyField( 'Customer', related_name= 'Goods' )
    begin_date = models.DateField(verbose_name="The time of the beginning of the buying",
                                  auto_now_add=True)
    pay_date = models.DateField(verbose_name="The time of the tiem to pay",
                                auto_now=True)
    count = models.PositiveBigIntegerField(verbose_name='Amount')
    total_cost = models.DecimalField(verbose_name='Common Price', 
                                     max_digits=10, decimal_places=2)
    is_pay = models.BooleanField(verbose_name='Bill fact',default=False)
   

    def __str__(self):
        return  f'{self.id_customer} - {self.id_goods}'

    class Meta:
        
      managed = True
      verbose_name = 'Sale'
      verbose_name_plural = 'Sales'
    pass


model_list = [ Brand, Customer , Goods , Sales ]