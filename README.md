# Пишу для себя:

## По поводу моей самописной ленивой загрузки сущностей прямо из БД:

В целом идея неплохая, но есть проблема, когда из БД загружаются сущности, связанные с одними и теми же сущностями (например, подтягиваем все продукты с определенного склада - все продукты будут ссылаться на один и тот же склад). Проблема в том, что если в слое Application, например, перебирать в цикле все продукты и обращаться от них к складу, склад (а я напоминаю - у всех продуктов он один и тот же) каждый раз будет подтягиваться из БД заново, несмотря на то, что нужный склад, по сути, уже был получен во время первой итерации цикла. Этот момент, конечно же, можно оптимизировать, если постараться, но пока что сойдет и так.
