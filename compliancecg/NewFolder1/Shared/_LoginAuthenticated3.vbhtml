﻿<style>
    html, body {
    font-family: Arial, Helvetica, sans-serif;
}

/* define a fixed width for the entire menu */
.navigation {
  width: 300px;
}

/* reset our lists to remove bullet points and padding */
.mainmenu, .submenu {
  list-style: none;
  padding: 0;
  margin: 0;
}

/* make ALL links (main and submenu) have padding and background color */
.mainmenu a {
  display: block;
  background-color: #CCC;
  text-decoration: none;
  padding: 10px;
  color: #000;
}

/* add hover behaviour */
.mainmenu a:hover {
    background-color: #C5C5C5;
}


/* when hovering over a .mainmenu item,
  display the submenu inside it.
  we're changing the submenu's max-height from 0 to 200px;
*/

.mainmenu li:hover .submenu {
  display: block;
  max-height: 200px;
}

/*
  we now overwrite the background-color for .submenu links only.
  CSS reads down the page, so code at the bottom will overwrite the code at the top.
*/

.submenu a {
  background-color: #999;
}

/* hover behaviour for links inside .submenu */
.submenu a:hover {
  background-color: #666;
}

/* this is the initial state of all submenus.
  we set it to max-height: 0, and hide the overflowed content.
*/
.submenu {
  overflow: hidden;
  max-height: 0;
  -webkit-transition: all 0.5s ease-out;
}
</style>
    



<nav class="navigation">
    <ul class="mainmenu">
        <li><a href="">Home</a></li>
        <li><a href="">About</a></li>
        <li>
            <a href="">Products</a>
            <ul class="submenu">
                <li><a href="">Tops</a></li>
                <li><a href="">Bottoms</a></li>
                <li><a href="">Footwear</a></li>
            </ul>
        </li>
        <li><a href="">Contact us</a></li>
    </ul>
</nav>