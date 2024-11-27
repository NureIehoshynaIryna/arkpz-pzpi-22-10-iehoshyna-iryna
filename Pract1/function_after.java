public void processUserWithDevices(User user) {
    validateUsersDevices(user);
    calculateSensorsTotal(user);
    saveUser(user);
}

private void validateUsersDevices(User user) {
    if (user.getDevices().isEmpty()) {
        throw new IllegalArgumentException("User doesn't have any device");
    }
}

private void calculateUsersSensorsTotal(User user) {
	int sencorCount = 0;
    for (Device device : user.getDevices()) {
        sencorCount += device.getSensorsCount();
    }
    user.setSensorCount(sencorCount);
}

private void saveUser(User user) {
    database.save(user);
}