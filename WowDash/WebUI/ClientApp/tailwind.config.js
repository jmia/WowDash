module.exports = {
    theme: {
      // These are default, simply for access
      screens: {
        sm: '640px',
        md: '768px',
        lg: '1024px',
        xl: '1280px'
      },
      extend: {
        colors: {
          'tan': '#C79C6E'
        }
      }
    },
    variants: {},
    plugins: [],
    purge: {
      enabled: process.env.NODE_ENV === 'production',
      content: [
        './public/**/*.html',
        './src/**/*.vue'
      ],
      options: {
        whitelistPatterns: [ 
      /-(leave|enter|appear)(|-(to|from|active))$/, 
      /^(?!(|.*?:)cursor-move).+-move$/,
          /^router-link(|-exact)-active$/
        ],
      },
      future: {
        removeDeprecatedGapUtilities: true,
      },
    }
  }