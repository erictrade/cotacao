# Histórico de Alterações #

## 2.4.3 ##

  * bug: a posição no monitor era guardada mesmo quando o _ticker_ fosse fechado quando estivesse oculto/minimizado, não sendo possível exibí-lo quando fosse reaberto
  * bug: campos numéricos são ordenados corretamente

## 2.4.2 ##

  * Guarda o tamanho da tela e posição no monitor
  * Melhorias no visual e otimização de espaço da tela
  * Se não houve variação, preço e código de negociação ficam na cor preta
  * bug: a versão 2.4.1 utilizava outra posição no registro para armazenar a lista de ações acompanhadas

## 2.4.1 ##

  * bug: campo "Variação" com erro para IBOV ou quando campo "Compra" estava zerado
  * _tooltips_ nas células do cabeçalho
  * tempo de consulta com 2 casas decimais
  * campos numéricos alinhados à direita
  * coluna "Negócios" agora com o número de negócios, ficando o valor financeiro no _tooltip_ das células
  * coluna "Variação" em negrito

## 2.4.0 ##

  * Inclui coluna "Preço de Fechamento"
  * Colunas são redimensionadas pelo tamanho da janela
  * Janela ocupa menos espaço
  * Preços ficam em vermelho ou azul de acordo com variação em relação ao preço de fechamento
  * _Tooltip_ das células de preço mostram variação em relação ao preço de fechamento
  * Webservice utiliza cache de 60 segundos, individualmente para cada papel
  * Coluna "Variação" sem atraso de 15 minutos
  * Coluna "Código" fica em vermelho ou azul de acordo com variação do preço atual
  * bug: agora a janela sempre aparece ao clicar no _trayicon_ para maximizá-la
  * documentação do webservice em XML

## 2.3 ##

  * obtém as configurações de proxy no Internet Explorer, funcionando em qualquer computador que tenha o _framework_ .NET

## 2.2 ##

  * bug: alguns tratamentos em casos de erros de conexão
  * não mostra mais símbolo R$ nos preços
  * linha azul ou vermelha de acordo com variação de preço

## 2.1 ##

  * traz a descrição do papel ao passar o mouse sobre seu código
  * também consulta cotação com dois cliques em célula vazia
  * não acessa o web service se não tem dados de papeis cadastrados
  * já abre com as cotações carregadas
  * minimiza/restaura ao clicar no ícone no _trayicon_
  * bug: não explode ao fechar, se alguma linha foi excluída

## 2.0 ##

  * código reconstruído e otimizado
  * guarda a lista de ações consultadas