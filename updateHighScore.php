<?php 
	echo "Connected to db";
	$con = mysqli_connect('localhost', 'root', '', 'unitycreds', 3302);

	// check that connection happened
	if (mysqli_connect_errno()) {
		echo "1 : Connection Failed"; //error code #1 = connection failed
		exit();
	}

	$username = $_POST["name"];
	$newHighScore = $_POST["newHighScoreString"];
	$sql = "UPDATE players SET highscore = '$newHighScore' WHERE username = '$username'";	
	$result = mysqli_query($con, $sql);
	echo "Updated successfully";
?>