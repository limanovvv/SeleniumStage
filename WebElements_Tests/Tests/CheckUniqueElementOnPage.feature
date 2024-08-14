# language: ru

Функционал: Проверка уникального элемента на странице

Сценарий: Проверка уникального элемента на странице кредита
  Когда открыть страницу по URL "<url>"
  Тогда проверить, что элемент с XPath "<xpath>" отображается
Примеры:
|url|xpath|
|https://ib.psbank.ru/store/products/consumer-loan|//*[contains(text(), 'Вернем все проценты по кредиту')]/ancestor::h1|

Примеры:
|url|xpath|
|https://ib.psbank.ru/store/products/investmentsbrokerage|//*[contains(text(), 'Инвестиции в ценные бумаги')]/ancestor::h1|