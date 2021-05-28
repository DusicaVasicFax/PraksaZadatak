export class User {
  firstname: string;
  lastname: string;
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
  phoneNumber: string;
  image: string;

  constructor(
    firstname: string,
    lastname: string,
    username: string,
    email: string,
    password: string,
    confirmPassword: string,
    phoneNumber: string,
    image: string
  ) {
    this.firstname = firstname;
    this.lastname = lastname;
    this.username = username;
    this.email = email;
    this.password = password;
    this.confirmPassword = confirmPassword;
    this.phoneNumber = phoneNumber;
    this.image = image;
  }
}
