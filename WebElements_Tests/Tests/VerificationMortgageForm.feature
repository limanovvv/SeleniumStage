﻿# language: ru

Функционал: проверка формы ипотеки

Сценарий: Проверка окна с сообщением об ошибке при нажатии на кнопку Заполнить без Госуслуг
  Дано Открыть страницу 'https://ib.psbank.ru/store/products/military-family-mortgage-program'
  Когда Нажать на кнопку с текстом 'Заполнить без Госуслуг'
  Тогда Проверить что появилась ошибка 'Оформление заявки станет доступным после заполнения обязательных полей'
  И Проверить что кнопка с сохраненным текстом отсутствует