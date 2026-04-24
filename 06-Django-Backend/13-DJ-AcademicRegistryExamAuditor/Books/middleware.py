#Books.middleware.py
from django.http import HttpResponse, HttpRequest
from django.contrib.auth.models import User


import random  as rnd
quotes_list = [
    'Be yourself; everyone else is already taken',
    'Change brings opportunity',
    'The secret of getting ahead is getting started',
    'Hope is NOT a strategy.',
    'Strive not to be a success, but rather to be of value.',
    'The future belongs to those who believe in the beauty of their dreams.',
    'Be not afraid of growing slowly; be afraid only of standing still.',
]


#"Books.middleware.gen_quote"
def gen_quote(function):

    def inner(req: HttpRequest) -> HttpResponse:
        #obrabotka zaprosa before next middleware
        resp : HttpResponse= function(req) #get the response 
        #obrabotka zaporsa after hte next middleware 
        
        #print(resp.content)
        print(resp.content.decode)
    
        html_txt = resp.content.decode("utf-8")
        #print(html_txt)
        temp_list = html_txt.split('\n')
        #print(temp_list)
        #choose quote
        quote = rnd.choice(quotes_list)
        temp_list.insert(-2, quote)#quote placing 
        html_txt = '\n'.join(temp_list)        # make one string 
        resp.content = html_txt.encode("utf-8") # from string to bite bynary content 
        return resp
        
        pass
    return inner


class ClearComment:
    def __init__(self, next_middleware):
        self.next_middleware = next_middleware
        pass
    
    
    #method for th eclass performance like a functional object 
    def __call__(self,req: HttpRequest)-> HttpResponse:
        #obrabotka before
        resp: HttpResponse = None
        resp = self.next_middleware(req)
        #obtabotka after
        html_text = resp.content.decode()
        new_text = ''
        start_pos = 0
        while True: 
            end_pos =   html_text.find('<!--', start_pos )
            if end_pos == -1: 
                break 
            new_text += html_text[start_pos : end_pos]
            #searcch the end of the comment
            start_pos = html_text.find('-->', end_pos)
            if start_pos == -1:
                break
            start += 3
        resp.content = new_text.encode() #clean website 
        return resp

        pass
        
    pass

#a = ClearComment()
#a(1,2,3)


#Books.middleware.template_gen_quote
def template_gen_quote(req) -> dict:
    res = {
        'quote'  : rnd.choice(quotes_list),
        'message': 'Hello!',
        'users': User,
    }
    return res
    pass