public double CountBill(User user){

    if (TextUtils.isEmpty(user.Name)) {
        System.out.println("User does't have name");
        return -1;
    }

    double sensorPrice = user.getSensorType().getSensorPrice();
	double userBill = user.sensorNumber * sensorPrice;
	return userBill;
}