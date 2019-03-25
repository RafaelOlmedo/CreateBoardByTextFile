# CreateBoardByTextFile

Repositório criado para estudo de manipulação de arquivos (texto, json) e criação de quadro.

## Funcionalidades

O projeto possui 2 principais funcionalidades:

1. Leitura de arquivo de texto que possui formatação pré definida de estimavativa de horas (versão 5.2) para soma das horas de *desenvolvimento* e *testes*.
2. Atualização de quadro no Trello, criando cartões para os tópicos presentes na estimativa do arquivo de texto.

## Instruções para utilização:

1. Baixar a versão mais atual do projeto:
>> https://github.com/RafaelOlmedo/CreateBoardByTextFile/releases

2. Compilar o projeto;

3. Copiar a pasta "Config" para dentro de "EfforteEstimation.Application\bin\Debug";

4. Configurar o arquivo "ConfigProperties.json" existente dentro da pasta "Config" (informações presente dentro de *ReadingTextFile* utilizadas para a leitura do arquivo de texto, já as presente dentro de *CreateBoard* são utilizadas para a atualização do quadro no Trello):
>> **FilePath**: Caminho onde se encontra o arquivo.
>>> No caminho, para cada barra, incluir uma barra adicional e no final do caminho incluir uma barra: **Ex** C:\\Users\\xpto\\Desktop\\Estimativas\\

>> **FileName**: Arquivo que deverá ser lido

>> **Create**: Indica se deverá ocorrer manipulação do quadro do Trello. Caso o objetivo seja somente obter a estimativa de horas, indicar 'false' no campo

>> **Key**: Chave de acesso para autenticação no Trello (https://trello.com/app-key)

>> **Token**: Token também utilizado para autenticação no Trello. Na mesma página onde se recupera a key, existe um link na palavra *token*. Ao clicar nela, será redirecionado para outra página onde será necessáro permitir acesso. Após clicar no botão para permitir, será redirecionado novamente para outra página, onde será exibido o token.

>> **IdBoard**: Id do quadro onde serão criados os cartões na coluna **Backlog**.

5. Executar o aplicativo.

6. O resultado pode ser visualizado no log criado dentro de "bin/Debug".
