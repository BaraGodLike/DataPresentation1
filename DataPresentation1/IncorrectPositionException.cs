namespace DataPresentation1;

// Класс исключения для некорректной позиции списка
public class IncorrectPositionException(string message) : Exception(message) {}