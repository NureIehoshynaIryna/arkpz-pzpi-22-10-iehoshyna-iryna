public class User {
	private String name;
	private String email;
	private String password;

	// ...

    public void displayProfile(Profile profile) {
        System.out.println("Name: " + name);
        System.out.println("Email: " + email);
        System.out.println("Age: " + profile.getAge());
    }
}

public class Profile {
    private int age;

    //...

    public int getAge() {
        return age;
    }
}