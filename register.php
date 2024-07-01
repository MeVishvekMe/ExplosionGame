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
	$namecheckquery = "SELECT username FROM players WHERE username='" . $username . "';";

	$namecheck = mysqli_query($con, $namecheckquery) or die("2 : Name query failed");

	if (mysqli_num_rows($namecheck) > 0) {
		echo "3 : username already exist";
		exit();
	}

	//add user to the table
	$insertuserquery = "INSERT INTO players(username, password, highscore) VALUES ('" . $username . "', '" . $password . "', 0);";
	mysqli_query($con, $insertuserquery) or die("4 : Insert data in DB failed");

	echo("0");
?>