int width;
int height;
String name;
String adress;
String email;

public int getMaxNumber(int a, int b, int c) {
	int max = 0;

	if (a > b && a > c) {
		max = a;
	}
	else if (b > a && b > c) {
		max = b;
	}
	else{
		max = c;
	}

    return max;
}