import { useState } from "react";
import { Link, useLocation } from "react-router-dom";
import { AppBar, Toolbar, Box, Button, IconButton } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import AppDrawer from "./AppDrawer";

const NAV_LINKS = [
  { label: "Home", to: "/" },
  { label: "Settings", to: "/settings" },
];

export default function Header() {
  const [drawerOpen, setDrawerOpen] = useState(false);
  const { pathname } = useLocation();

  return (
    <>
      <AppBar position="sticky" color="default">
        <Toolbar sx={{ gap: 1 }}>
          <Box
            component={Link}
            to="/"
            sx={{
              fontWeight: 700,
              fontSize: "1.1rem",
              color: "inherit",
              textDecoration: "none",
              mr: 2,
            }}
          >
            Clean Fullstack
          </Box>

          <Box
            sx={{ display: { xs: "none", sm: "flex" }, gap: 0.5, flexGrow: 1 }}
          >
            {NAV_LINKS.map(({ label, to }) => (
              <Button
                key={to}
                component={Link}
                to={to}
                color="inherit"
                sx={{ fontWeight: pathname === to ? 700 : 400 }}
              >
                {label}
              </Button>
            ))}
          </Box>

          <Box sx={{ flexGrow: { xs: 1, sm: 0 } }} />

          <IconButton
            color="inherit"
            onClick={() => setDrawerOpen(true)}
            sx={{ display: { sm: "none" } }}
          >
            <MenuIcon />
          </IconButton>
        </Toolbar>
      </AppBar>

      <AppDrawer open={drawerOpen} onClose={() => setDrawerOpen(false)} />
    </>
  );
}
