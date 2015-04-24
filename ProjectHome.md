Aplicativo (_ticker_) em C# para acompanhar as cotações de ações na BM&FBovespa em tempo real (atraso de 15 minutos em alguns dados). Um único acesso é feito à Internet para cada consulta de cotação de todos os papeis acompanhados.

## Início ##
Tudo começou com o curso [2524C da Microsoft (Developing XML Web Services using Microsoft ASP.NET)](http://www.microsoft.com/learning/en/us/syllabi/2524Cfinal.mspx). Quando eu concluí o curso, estava tão empolgado que no dia seguinte criei um webservice que acessava cotações de ações Bovespa publicamente disponíveis na Internet e retornava-as devidamente agrupadas e formatadas.

Um colega de trabalho havia construído um _ticker_ em VB6, que usava para acompanhar discretamente as cotações durante o dia.

Primeiramente tentei adaptar o _ticker_ dele para usar meu webservice, mas depois de meia hora olhando o código fonte, resolvi criar um do zero em C#.

Assim que a primeira versão do meu _ticker_ estava pronta, alterei o webservice para que ele recebesse uma lista de códigos de papéis e retornasse uma lista de cotações. Assim com um único acesso à Internet, enviando alguns kbytes de informação, eu receberia a cotação completa de todos os papéis que quisesse acompanhar.

## Distribuição ##
Primeiro eu passei o executável do _ticker_ para meus colegas de trabalho, depois disponibilizei em [meu blog](http://holococos.sjdr.com.br/dinheiro/cotacao-bovespa/) e agora disponibilizo aqui, junto com o código fonte.

## Idéias ##
Ainda tenho algumas idéias de melhorias para o webservice e o _ticker_.

  * fazer uma versão do web service em Phyton usando o Google App Engine
  * portar o ticker para gadget, wap, HTML/DHTML/Ajax, fazer uma versão para o iPhone
  * implementar [melhorias sugeridas pelos usuários](http://code.google.com/p/cotacao/issues/list)
  * alterar o web service para utilizar fontes alternativas de cotação, como o [Yahoo Finance](http://finance.yahoo.com/)