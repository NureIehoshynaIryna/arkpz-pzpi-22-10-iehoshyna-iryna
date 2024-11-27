public class User {
	private String name;
	private String email;
	private String password;

	// ...
}

public class Profile {
    private int age;

    public void displayProfile(User user) {
	    System.out.println("Name: " + user.getName());
	    System.out.println("Email: " + user.getEmail());
	    System.out.println("Age: " + age);
	}
}