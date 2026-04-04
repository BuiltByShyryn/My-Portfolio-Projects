from django.shortcuts import render
from django.http import HttpRequest,HttpResponse
import requests 
# Create your views here.
import json 
import random as rnd
#from django.contrib.admin import
from django.contrib.auth.models import User, Group
from django.views.generic import TemplateView
from django.contrib.auth.decorators import login_required,permission_required,user_passes_test





class v_user_info(TemplateView):
    
    template_name='ueers_info.html'
    @login_required(login_url = 'login', redirect_field_name = 'users_info')
    #@user_passes_test(lambda user: user.is_staff is True, login_url='login')
    #@user_passes_test(lambda user: 'gr_doctor' in user.groups, login_url='login')
    def get(self, req: HttpRequest)->HttpResponse:
        if not req.user.is_authenticated:
            return render(req, 'login.html',{})
        
        groups = Group.objects.all()
        users = User.objects.all()
       
        return self.render_to_response({'users': users, 'groups': groups})
        
        pass

def v_home(req: HttpRequest)-> HttpResponse: 
    return render(req,'index.html' )
    pass
@login_required(login_url = 'login')
def v_api_integer(req: HttpRequest)-> HttpResponse: 
    #https://www.random.org/integers/?num=100&min=1&max=6&col=10&base=10&format=plain&rnd=new
    url = 'https://www.random.org/integers/'
    param = {
        'num' : 20,
        'min' : 1,
        'max' : 100,
        'col' : 20,
        'base' : 10, #2/8/10/16
        'format': 'plain',
        'rnd': 'new',
    }
    resp = requests.get(url, param) #request.post()
    text = resp.content.decode() #bytes ===text
    print('res.random:', text)
    
    numbers = text.split() #['2','41', '5', ...]
    #get the chastotnyi slovarb 
    res = {}
    for key in set(numbers):
        res[key] = numbers.count(key)
    
    result = json.dumps(res)
    return HttpResponse(result)

    
def v_api_name(req: HttpRequest)-> HttpResponse:
    count = 10
    result = {}

    for x in range(count):

        name_len = rnd.randint(4,8)

        name_res = chr(ord('A') + rnd.randrange(26))

        for i in range(name_len-1):
            name_res += chr(ord('a') + rnd.randrange(26))

        result[x] = name_res

    result['status'] = 'OK'
    result['count'] = count
    result['start'] = 0
    result['end'] = count-1

    return HttpResponse(json.dumps(result))



def v_api_iin(req: HttpRequest) -> HttpResponse:
    import random
    from datetime import datetime, timedelta
    import json

    
    random_date = lambda: (datetime(1900,1,1) + timedelta(days=random.randint(0, 45000))).strftime('%y%m%d')

    yy_mm_dd = random_date()
    rr = f"{random.randint(1,16):02d}"        
    ss = f"{random.randint(0,99):02d}"        
    c = random.choice([1,2,3,4])              
    
    iin_without_k = f"{yy_mm_dd}{rr}{ss}{c}"
    
    
    k = sum([int(d)*w for d,w in zip(iin_without_k, range(1,12))]) % 11
    if k == 10:
        k = 0

    iin = iin_without_k + str(k)

    return HttpResponse(json.dumps({"iin": iin}))