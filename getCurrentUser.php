<?php 
	$con = mysqli_connect('localhost', 'root', '', 'unitycreds', 3302);

	// check that connection happened
	if (mysqli_connect_errno()) {
		echo "1 : Connection Failed"; //error code #1 = connection failed
		exit();
	}

	$username = $_POST["name"];
	$sql1 = "SELECT username, highscore FROM players WHERE username = '$username';";	
	$result1 = mysqli_query($con, $sql1);
	$loginInfo = mysqli_fetch_assoc($result1);
	echo $loginInfo['highscore'];
?>