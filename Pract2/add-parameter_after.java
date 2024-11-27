public double CountBill(User user, int discountAmount = 0){

    if (TextUtils.isEmpty(user.Name)) {
        System.out.println("User does't have name");
        return -1;
    }

    double sensorPrice = user.getSensorType().getSensorPrice();
	double userBill = user.sensorNumber * sensorPrice;
	if (userBill >= 1000) {
	    userBill -= discountAmount;
	}
	return userBill;
}

