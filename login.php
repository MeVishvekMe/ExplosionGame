<?php 
	$con = mysqli_connect('localhost', 'root', '', 'unitycreds', 3302);

	// check that connection happened
	if (mysqli_connect_errno()) {
		echo "1 : Connection Failed"; //error code #1 = connection failed
		exit();
	}

	$username = $_POST["name"];
	$password = $_POST["password"];

	//check if name exists
	$namecheckquery = "SELECT username, password FROM players WHERE username='" . $username . "';";

	$namecheck = mysqli_query($con, $namecheckquery) or die("2 : Name query failed");

	if (mysqli_num_rows($namecheck) != 1) {
		echo "5 : Either no user with name, or more than one";
		exit();
	}

	$loginInfo = mysqli_fetch_assoc($namecheck);
	$loginPass = $loginInfo["password"];

	if ($loginPass != $password) {
		echo "6 : Incorrect password";
		exit();
	}
	echo "0";
?>