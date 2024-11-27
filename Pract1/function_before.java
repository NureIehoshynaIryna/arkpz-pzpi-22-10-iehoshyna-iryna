public void handleUsersDevices(User user) {
    // Валідація наявності девайсів у користувача
    if (user.getDevices().isEmpty()) {
        throw new IllegalArgumentException("User doesn't have any device");
    }
    
    // Розрахунок кількості сенсорів у користувача
    int sencorCount = 0;
    for (Device device : user.getDevices()) {
        sencorCount += device.getSensorsCount();
    }
    user.setSensorCount(sencorCount);

    // Збереження користувача
    database.save(user);
}