/** @type {import('tailwindcss').Config} */ // cspell:disable-line
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'white': '#FFFFFF',
        'blue': '#61DAFB',
        'purple': '#7e5bef',
        'pink': '#ff49db',
        'orange': '#ff7849',
        'green': '#13ce66',
        'yellow': '#ffc82c',
        'gray-dark': '#273444',
        'gray': '#8492a6',
        'gray-light': '#d3dce6',
      }
    },
  },
  plugins: [
    require('@tailwindcss/forms'), // cspell:disable-line
    ({ addComponents }) => {
      const AppLogo = {
        '.App-logo': {
          animation: 'App-logo-spin infinite 20s linear',
          height: '40vmin', // cspell:disable-line
          'pointer-events': 'none'
        },
        '@keyframes App-logo-spin': {
          from: {
            transform: 'rotate(0deg)'
          },
          to: {
            transform: 'rotate(360deg)'
          }
        }
      }
      addComponents(AppLogo)
    }
  ],
}