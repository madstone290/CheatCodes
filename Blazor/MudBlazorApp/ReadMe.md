﻿
## NavigationManager cheatsheet
- 현재경로
	- MyNavigationManager.Uri
	- https://localhost:5001/counter/3?q=hi
- 베이스경로
	-MyNavigationManager.BaseUri
	- https://localhost:5001/
- 경로 이동
	- MyNavigationManager.NavigateTo("http://newlocation")
	- Navigates to new location
- 경로 변경 탐색
	- MyNavigationManager.LocationChanged
	- An event that fires when the navigation location has changed.
- 절대 경로 
	- MyNavigationManager.ToAbsoluteUri("pepe")
	- https://localhost:5001/pepe
- 상대 경로
	- MyNavigationManager.ToBaseRelativePath(MyNavigationManager.Uri)
	- counter/3?q=hi
- 쿼리 확장 메소드
	- AddQueryParm( "q2", "bye" ) // (*1)
	- https://localhost:5001/counter/3?q=hi&q2=bye
	- GetQueryParm( "q" )
	- hi